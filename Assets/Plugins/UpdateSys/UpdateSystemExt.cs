using UnityEngine;

namespace UpdateSys {
   public static class UpdateSystemExt {
      /// <summary>
      /// Enables OnSystemUpdate to be executed on IUpdatable
      /// </summary>
      public static void StartUpdate(this IUpdatable element) => UpdateSystem.StartUpdate(element);

      /// <summary>
      /// Enables OnSystemUpdate to be executed on IUpdatable
      /// </summary>
      public static void StopUpdate(this IUpdatable element) => UpdateSystem.StopUpdate(element);

      /// <summary>
      /// Enables OnSystemFixedUpdate to be executed on IUpdatable
      /// </summary>
      public static void StartFixedUpdate(this IFixedUpdatable element) => UpdateSystem.StartFixedUpdate(element);

      /// <summary>
      /// Disables OnSystemFixedUpdate
      /// </summary>
      public static void StopFixedUpdate(this IFixedUpdatable element) => UpdateSystem.StopFixedUpdate(element);

      /// <summary>
      /// Enables OnSystemLateUpdate
      /// </summary>
      public static void StartLateUpdate(this ILateUpdatable element) => UpdateSystem.StartLateUpdate(element);

      /// <summary>
      /// Disables OnSystemFixedUpdate
      /// </summary>
      public static void StopLateUpdate(this ILateUpdatable element) => UpdateSystem.StopLateUpdate(element);

      public static void StartTickUpdate(this ITickUpdatable element) => TickUpdateSystem.StartTickUpdate(element);
      
      public static void StopTickUpdate(this ITickUpdatable element) => TickUpdateSystem.StopTickUpdate(element);

      public static void StartSecondUpdate(this ISecondUpdatable element) => UpdateSystem.StartSecondUpdate(element);
      
      public static void StopSecondUpdate(this ISecondUpdatable element) => UpdateSystem.StopSecondUpdate(element);

      /// <summary>
      /// Attempts to start all available updates
      /// </summary>
      public static void StartAllUpdates(this IAnyUpdatable element) {
         IUpdatable updatable = element as IUpdatable;
         updatable?.StartUpdate();
         
         ISecondUpdatable secondUpdatable = element as ISecondUpdatable;
         secondUpdatable?.StartSecondUpdate();

         IFixedUpdatable fixedUpdatable = element as IFixedUpdatable;
         fixedUpdatable?.StartFixedUpdate();

         ILateUpdatable lateUpdatable = element as ILateUpdatable;
         lateUpdatable?.StartLateUpdate();

         ITickUpdatable tickUpdatable = element as ITickUpdatable;
         tickUpdatable?.StartTickUpdate();
      }

      /// <summary>
      /// Attempts to stop all available updates
      /// </summary>
      public static void StopAllUpdates(this IAnyUpdatable element) {
         IUpdatable updatable = element as IUpdatable;
         updatable?.StopUpdate();
         
         ISecondUpdatable secondUpdatable = element as ISecondUpdatable;
         secondUpdatable?.StopSecondUpdate();

         IFixedUpdatable fixedUpdatable = element as IFixedUpdatable;
         fixedUpdatable?.StopFixedUpdate();

         ILateUpdatable lateUpdatable = element as ILateUpdatable;
         lateUpdatable?.StopLateUpdate();
         
         ITickUpdatable tickUpdatable = element as ITickUpdatable;
         tickUpdatable?.StopTickUpdate();
      }

      /// <summary>
      /// Attempts to start all available updates
      /// </summary>
      public static void StartAllUpdates(this MonoBehaviour element) {
         IUpdatable updatable = element as IUpdatable;
         updatable?.StartUpdate();

         IFixedUpdatable fixedUpdatable = element as IFixedUpdatable;
         fixedUpdatable?.StartFixedUpdate();

         ILateUpdatable lateUpdatable = element as ILateUpdatable;
         lateUpdatable?.StartLateUpdate();
      }

      /// <summary>
      /// Attempts to stop all available updates
      /// </summary>
      public static void StopAllUpdates(this MonoBehaviour element) {
         IUpdatable updatable = element as IUpdatable;
         updatable?.StopUpdate();

         IFixedUpdatable fixedUpdatable = element as IFixedUpdatable;
         fixedUpdatable?.StopFixedUpdate();

         ILateUpdatable lateUpdatable = element as ILateUpdatable;
         lateUpdatable?.StopLateUpdate();
      }
   }
}