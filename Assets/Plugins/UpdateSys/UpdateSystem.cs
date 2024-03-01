using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Profiling;

namespace UpdateSys {
   /// <summary>
   /// Basic update manager that handles Update / FixedUpdate scenarios
   /// </summary>
   public static class UpdateSystem {
      public static float TimeScale { get; set; } = 1f;
      public static float TimeTotal { get; private set; }

      private static readonly HashSet<IUpdatable> _updatables = new HashSet<IUpdatable>();
      private static readonly HashSet<IUpdatable> _updateInsertBuffer = new HashSet<IUpdatable>();
      private static readonly HashSet<IUpdatable> _updateRemoveBuffer = new HashSet<IUpdatable>();
      
      private static readonly HashSet<ISecondUpdatable> _secondUpdatables = new HashSet<ISecondUpdatable>();
      private static readonly HashSet<ISecondUpdatable> _secondUpdateInsertBuffer = new HashSet<ISecondUpdatable>();
      private static readonly HashSet<ISecondUpdatable> _secondUpdateRemoveBuffer = new HashSet<ISecondUpdatable>();

      private static readonly HashSet<IFixedUpdatable> _fixedUpdatables = new HashSet<IFixedUpdatable>();
      private static readonly HashSet<IFixedUpdatable> _fixedUpdateInsertBuffer = new HashSet<IFixedUpdatable >();
      private static readonly HashSet<IFixedUpdatable> _fixedUpdateRemoveBuffer = new HashSet<IFixedUpdatable>();

      private static readonly HashSet<ILateUpdatable> _lateUpdatables = new HashSet<ILateUpdatable>();
      private static readonly HashSet<ILateUpdatable> _lateUpdateInsertBuffer = new HashSet<ILateUpdatable>();
      private static readonly HashSet<ILateUpdatable> _lateUpdateRemoveBuffer = new HashSet<ILateUpdatable>();

      [RuntimeInitializeOnLoadMethod]
      private static void Init() {
         MainThreadDispatcher.StartUpdateMicroCoroutine(DoUpdate());
         MainThreadDispatcher.StartFixedUpdateMicroCoroutine(DoFixedUpdate());
         MainThreadDispatcher.StartLateUpdateMicroCoroutine(DoLateUpdate());
      }

      private static IEnumerator DoUpdate() {
         while (true) {
            float deltaTime = Time.deltaTime * TimeScale;
            TimeTotal += deltaTime;

            Profiler.BeginSample("UpdateSystem::OnUpdate");
            DoUpdateLogic(_updatables, _updateRemoveBuffer, _updateInsertBuffer, deltaTime);
            Profiler.EndSample();

            Profiler.BeginSample("UpdateSystem::OnSecondUpdate");
            DoSecondUpdateLogic(_secondUpdatables, _secondUpdateRemoveBuffer, _secondUpdateInsertBuffer, deltaTime);
            Profiler.EndSample();

            yield return null;
         }
      }

      private static void DoUpdateLogic(HashSet<IUpdatable> updatables,
                                        HashSet<IUpdatable> removeBuffer,
                                        HashSet<IUpdatable> insertBuffer,
                                        float deltaTime) {
         foreach (IUpdatable element in removeBuffer) {
            updatables.Remove(element);
         }

         removeBuffer.Clear();

         foreach (IUpdatable element in insertBuffer) {
            updatables.Add(element);
         }

         insertBuffer.Clear();

         foreach (IUpdatable element in updatables) {
            try {
               element.OnSystemUpdate(deltaTime);
            } catch (Exception ex) {
               Debug.LogError($"[Update] Unhandled ex:: {ex.Message} Stack: {ex.StackTrace}");
               removeBuffer.Add(element);
            }
         }
      }

      private static void DoSecondUpdateLogic(HashSet<ISecondUpdatable> updatables,
                                              HashSet<ISecondUpdatable> removeBuffer,
                                              HashSet<ISecondUpdatable> insertBuffer,
                                              float deltaTime) {
         foreach (ISecondUpdatable element in removeBuffer) {
            updatables.Remove(element);
         }

         removeBuffer.Clear();

         foreach (ISecondUpdatable element in insertBuffer) {
            updatables.Add(element);
         }

         insertBuffer.Clear();

         foreach (ISecondUpdatable element in updatables) {
            try {
               element.OnSystemSecondUpdate(deltaTime);
            } catch (Exception ex) {
               Debug.LogError($"[Update] Unhandled ex:: {ex.Message} Stack: {ex.StackTrace}");
               removeBuffer.Add(element);
            }
         }
      }

      private static IEnumerator DoFixedUpdate() {
         while (true) {
            Profiler.BeginSample("UpdateSystem::FixedUpdate");
            foreach (IFixedUpdatable element in _fixedUpdateRemoveBuffer) {
               _fixedUpdatables.Remove(element);
            }

            _fixedUpdateRemoveBuffer.Clear();

            foreach (IFixedUpdatable element in _fixedUpdateInsertBuffer) {
               _fixedUpdatables.Add(element);
            }

            _fixedUpdateInsertBuffer.Clear();

            float deltaTime = Time.fixedDeltaTime * TimeScale;

            foreach (IFixedUpdatable element in _fixedUpdatables) {
               try {
                  element.OnSystemFixedUpdate(deltaTime);
               } catch (Exception ex) {
                  Debug.LogError($"[FixedUpdate] Unhandled ex:: {ex.Message} Stack: {ex.StackTrace}");
                  _fixedUpdateRemoveBuffer.Add(element);
               }
            }

            Profiler.EndSample();
            yield return null;
         }
      }

      private static IEnumerator DoLateUpdate() {
         while (true) {
            Profiler.BeginSample("UpdateSystem::LateUpdate");
            foreach (ILateUpdatable element in _lateUpdateRemoveBuffer) {
               _lateUpdatables.Remove(element);
            }

            _lateUpdateRemoveBuffer.Clear();

            foreach (ILateUpdatable element in _lateUpdateInsertBuffer) {
               _lateUpdatables.Add(element);
            }

            _lateUpdateInsertBuffer.Clear();

            float deltaTime = Time.deltaTime * TimeScale;

            foreach (ILateUpdatable element in _lateUpdatables) {
               try {
                  element.OnSystemLateUpdate(deltaTime);
               } catch (Exception ex) {
                  Debug.LogError($"[LateUpdate] Unhandled ex:: {ex.Message} Stack: {ex.StackTrace}");
                  _lateUpdateRemoveBuffer.Add(element);
               }
            }

            Profiler.EndSample();
            yield return null;
         }
      }

      /// <summary>
      /// Enables OnSystemUpdate to be executed on IUpdatable
      /// </summary>
      public static void StartUpdate(IUpdatable updatable) {
         _updateInsertBuffer.Add(updatable);
         _updateRemoveBuffer.Remove(updatable);
      }
      
      /// <summary>
      /// Disables OnSystemUpdate on IUpdatable 
      /// </summary>
      public static void StopUpdate(IUpdatable updatable) {
         _updateRemoveBuffer.Add(updatable);
         _updateInsertBuffer.Remove(updatable);
      }

      /// <summary>
      /// Enables OnSystemSecondUpdate to be executed on ISecondUpdatable
      /// </summary>
      public static void StartSecondUpdate(ISecondUpdatable updatable) {
         _secondUpdateInsertBuffer.Add(updatable);
         _secondUpdateRemoveBuffer.Remove(updatable);
      }
      
      /// <summary>
      /// Disables OnSystemSecondUpdate on ISecondUpdatable 
      /// </summary>
      public static void StopSecondUpdate(ISecondUpdatable updatable) {
         _secondUpdateRemoveBuffer.Add(updatable);
         _secondUpdateInsertBuffer.Remove(updatable);
      }

      /// <summary>
      /// Enables OnSystemUpdate to be executed on IFixedUpdatable
      /// </summary>
      public static void StartFixedUpdate(IFixedUpdatable updatable) {
         _fixedUpdateInsertBuffer.Add(updatable);
         _fixedUpdateRemoveBuffer.Remove(updatable);
      }

      /// <summary>
      /// Disables OnSystemFixedUpdate on IFixedUpdatable
      /// </summary>
      public static void StopFixedUpdate(IFixedUpdatable updatable) {
         _fixedUpdateRemoveBuffer.Add(updatable);
         _fixedUpdateInsertBuffer.Remove(updatable);
      }

      /// <summary>
      /// Enables OnSystemLateUpdate on ILateUpdatable
      /// </summary>
      public static void StartLateUpdate(ILateUpdatable updatable) {
         _lateUpdateInsertBuffer.Add(updatable);
         _lateUpdateRemoveBuffer.Remove(updatable);
      }

      /// <summary>
      /// Disables OnSystemLateUpdate on ILateUpdatable
      /// </summary>
      public static void StopLateUpdate(ILateUpdatable updatable) {
         _lateUpdateRemoveBuffer.Add(updatable);
         _lateUpdateInsertBuffer.Remove(updatable);
      }
   }
}