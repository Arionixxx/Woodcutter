namespace UpdateSys {
   /// <summary>
   /// Contract for the element behaviour that should run based on the tick delay instead of each frame.
   /// </summary>
   public interface ITickUpdatable : IAnyUpdatable {
      float TimeBeforeTickUpdate { get; set; }
      float UpdateTickRate { get; }

      /// <summary>
      /// Called when time delay expires
      /// </summary>
      void OnSystemTickUpdate(float dt);
   }
}
