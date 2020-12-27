using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    //칸들은 8x8로 구성되어 있습니다.
    public List<CellArray> lines;

    private void Awake()
    {
        lines = new List<CellArray>();
        for(int i = 0;i < 8;i++)
        {
            CellArray item = new CellArray();
            item.cells = new List<Transform>();
            for(int j = 0; j < 8;j++)
            {
                item.cells.Add(null);
            }
            lines.Add(item);
        }

        int index = 0;
        foreach(Transform child in this.transform)
        {
            //어떤 라인인지 결정
            int line = index / 8; // 0~7까지는 0, 8~15 까지는 1, 16~23까지는 2 ....
            //몇번째 칸인지 결정
            int cellNum = index % 8;// 0~7까지는 그대로, 8이 되는 순간 다시 0부터 시작
            lines[line].cells[cellNum] = child;
            index++;
        }
    }

    public void MoveUnit(GameObject target, int line, int cellNum)
    {
        if(line < 0 || line >= lines.Count)
        {
            return;
        }
        if(cellNum < 0 || cellNum >= lines[line].cells.Count)
        {
            return;
        }

        target.transform.parent = lines[line].cells[cellNum];
        target.transform.localPosition = Vector3.zero;
        target.transform.localScale = Vector3.one;
        target.transform.localRotation = Quaternion.identity;

        Unit script = target.GetComponent<Unit>();
        script.curLine = line;
        script.curCellNum = cellNum;
    }
}

[System.Serializable]
public class CellArray
{
    public List<Transform> cells;
}
