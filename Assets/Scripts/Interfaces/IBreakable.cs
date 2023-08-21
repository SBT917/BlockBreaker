using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボールによる破壊が可能なオブジェクトに実装するインターフェース
public interface IBreakable
{
    //破壊時処理
    public void Break();
}
