using PracticeProj.Src.Cont;
using System.Collections.Generic;

namespace PracticeProj.Src.Cont
{
    internal class CccWakeMdl
    {
        public int row;
        public class Pos
        {
            public int no; //pos#
            public float x;
            public float y;
            public string time;
        }
        public List<Pos> pos = new List<Pos>();
    }
}

////モデルリスト
//private List<CccWakeMdl> cCccWakeMdlList;
//
// cCccWakeMdlList 例
// { 
//    aWake1:{ 
//        info:{ id: 1 }, 
//        pos1:{ x: 120.007 , y: 35.846 , 'time': '20230101001111' }, 
//        pos2:{ x: 124.496 , y: 33.370 , 'time': '20230101001122' }, 
//        pos3:{ x: 121.259 , y: 31.974 , 'time': '20230101001133' }, 
//        pos4:{ x: 123.925 , y: 30.197 , 'time': '20230101001144' } 
//    }, 
//    aWake2:{ 
//        info:{ id: 2 }, 
//        pos1:{ x: 136.700 , y: 39.000 , 'time': '20230101001100' }, 
//        pos2:{ x: 136.200 , y: 39.000 , 'time': '20230101001110' }, 
//        pos3:{ x: 135.500 , y: 38.600 , 'time': '20230101001120' }, 
//        pos4:{ x: 134.800 , y: 38.500 , 'time': '20230101001130' } 
//    }, 
//    aWake3:{ 
//        info:{ id: 3 }, 
//        pos1:{ x: 143.855 , y: 34.703 , 'time': '20230102001111' }, 
//        pos2:{ x: 145.505 , y: 33.307 , 'time': '20230102001122' }, 
//        pos3:{ x: 143.030 , y: 32.545 , 'time': '20230102001133' }, 
//        pos4:{ x: 145.378 , y: 31.276 , 'time': '20230102001144' } 
//    } 
// }