namespace UpdateSys {
   public interface ISecondUpdatable : IAnyUpdatable {
      void OnSystemSecondUpdate(float deltaTime);
   }
}