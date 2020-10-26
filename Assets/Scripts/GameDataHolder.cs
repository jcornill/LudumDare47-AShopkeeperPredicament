using UnityEngine;

public class GameDataHolder : MonoBehaviour
{
   public GameData GameData;

   public static GameData data;

   public void Awake()
   {
      data = this.GameData;
   }
}
