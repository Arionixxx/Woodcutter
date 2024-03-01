using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Profiling;
using Random = UnityEngine.Random;

namespace UpdateSys {
   /// <summary>
   /// Similar to UpdateSystem, TickUpdateSystem runs at specific intervals based on its ITickUpdatable values
   /// </summary>
   public static class TickUpdateSystem {
      private static readonly HashSet<ITickUpdatable> _tickables = new HashSet<ITickUpdatable>();
      private static readonly HashSet<ITickUpdatable> _insertBuffer = new HashSet<ITickUpdatable>();
      private static readonly HashSet<ITickUpdatable> _removeBuffer = new HashSet<ITickUpdatable>();

      private const float DistributionMinRange = -0.02f;
      private const float DistributionMaxRange = 0.02f;

      [RuntimeInitializeOnLoadMethod]
      private static void Init() {
         MainThreadDispatcher.StartUpdateMicroCoroutine(DoUpdate());
      }

      private static IEnumerator DoUpdate() {
         while (true) {
            Profiler.BeginSample("UpdateSystem::Update");
            foreach (ITickUpdatable element in _removeBuffer) {
               _tickables.Remove(element);
            }

            _removeBuffer.Clear();

            foreach (ITickUpdatable element in _insertBuffer) {
               _tickables.Add(element);
            }

            _insertBuffer.Clear();

            float deltaTime = Time.deltaTime;

            foreach (ITickUpdatable element in _tickables) {
               try {
                  float time = element.TimeBeforeTickUpdate - deltaTime;

                  if (time <= 0) {
                     element.OnSystemTickUpdate(deltaTime);
                     time = element.UpdateTickRate + Random.Range(DistributionMinRange, DistributionMaxRange);
                  }

                  element.TimeBeforeTickUpdate = time;
               } catch (Exception ex) {
                  Debug.LogError($"[Update] Unhandled ex:: {ex.Message} Stack: {ex.StackTrace}");
                  _removeBuffer.Add(element);
               }
            }

            Profiler.EndSample();
            yield return null;
         }
      }

      /// <summary>
      /// Enables OnSystemTickUpdate to be executed on ITickUpdatable
      /// </summary>
      public static void StartTickUpdate(ITickUpdatable updatable) {
         _insertBuffer.Add(updatable);
         _removeBuffer.Remove(updatable);
      }
      
      /// <summary>
      /// Disables OnSystemTickUpdate on ITickUpdatable 
      /// </summary>
      public static void StopTickUpdate(ITickUpdatable updatable) {
         _removeBuffer.Add(updatable);
         _insertBuffer.Remove(updatable);
      }
   }
}