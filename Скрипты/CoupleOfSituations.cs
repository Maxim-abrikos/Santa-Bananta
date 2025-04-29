using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoupleOfSituations //:MonoBehaviour
{
    [SerializeField]
    internal List<FewSituations> CoupSituations = new List<FewSituations>();
    private enum Character_ {Полковник, Активист, Корпорат, Мэр};
}
