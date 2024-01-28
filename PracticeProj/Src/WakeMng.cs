using GeoAPI.Geometries;
using SharpMap.Data.Providers;
using SharpMap.Data;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetTopologySuite.Geometries;
using SharpMap.Forms;
using System.Collections.ObjectModel;

namespace PracticeProj.Src
{
    public class WakeMng
    {
        UctrlMap m_cUctrlMap; //地図操作オブジェクト
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> g_dicAWake; //ディクショナリー
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> g_dicSlctAWake; //ディクショナリー(時刻連動選択用)

        //航跡のコンフィグ
        public class WakeCfg
        {
            public string sLyrName { set; get; } //レイヤ名
            public bool isPoint { set; get; } //ポイント描画の有無
            public System.Drawing.Brush pointColor { set; get; } //ポイント色
            public float pointSize { set; get; } //ポイントサイズ
            public bool isLine { set; get; } //ライン描画の有無
            public System.Drawing.Color lineColor { set; get; } //ライン色
            public float lineWidth { set; get; } //ラインの太さ
            public bool isLineDash { set; get; } //ラインを破線にするかどうか
            public bool isLineArrow { set; get; } //ラインを破線にするかどうか
            public bool isLabel { set; get; } //ラベル描画の有無
            public System.Drawing.Color labelBackColor { set; get; } //ラベル背景色
            public System.Drawing.Color labelForeColor { set; get; } //ラベル背景色
        }
        private WakeCfg g_cfgAWake = new WakeCfg();
        private WakeCfg g_cfgSlctAWake = new WakeCfg();

        //地図操作オブジェクトセット
        public void SetUctrlMap(UctrlMap cUctrlMap)
        {
            m_cUctrlMap = cUctrlMap;
        }

        //航跡初期化
        public void InitWake(
            string scene,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dicAWake,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dicDTrck,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dicBWake,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> dicCPlac
            )
        {
            MessageBox.Show("WakeMng::InitWake");

            //mapBoxの初期化
            m_cUctrlMap.InitLayerOtherThanBase();

            //コンフィグを初期化
            InitWakeConfig();

            switch (scene)
            {
                case "Ccc":
                    //コンフィグ変更
                    g_cfgAWake.lineColor = System.Drawing.Color.Red;
                    g_cfgSlctAWake.pointColor = System.Drawing.Brushes.Orange;

                    //Mapに描画する
                    GenerateWakeLayer(ref dicAWake, ref g_cfgAWake);

                    //空(から)の選択用レイヤを生成
                    //GenerateSelectLayer(ref g_cfgSlctAWake);

                    //mapBoxを再描画
                    m_cUctrlMap.MapBoxRefresh();
                    break;
            }
        }

        //コンフィグを初期化
        private void InitWakeConfig()
        {
            //=====未選択用=====

            g_cfgAWake.sLyrName = "layAWake";
            g_cfgAWake.isPoint = false;
            g_cfgAWake.pointColor = System.Drawing.Brushes.White;
            g_cfgAWake.pointSize = 0;
            g_cfgAWake.isLine = true;
            g_cfgAWake.lineColor = System.Drawing.Color.LightPink;
            g_cfgAWake.lineWidth = 1;
            g_cfgAWake.isLineDash = false;
            g_cfgAWake.isLineArrow = true;
            g_cfgAWake.isLabel = true;
            g_cfgAWake.labelBackColor = System.Drawing.Color.LightPink;
            g_cfgAWake.labelForeColor = System.Drawing.Color.Black;

            //=====選択用=====

            g_cfgSlctAWake.sLyrName = "laySelectAWake";
            g_cfgSlctAWake.isPoint = false;
            g_cfgSlctAWake.pointColor = System.Drawing.Brushes.Red;
            g_cfgSlctAWake.pointSize = 5;
            g_cfgSlctAWake.isLine = true;
            g_cfgSlctAWake.lineColor = System.Drawing.Color.Red;
            g_cfgSlctAWake.lineWidth = 2;
            g_cfgSlctAWake.isLineDash = false;
            g_cfgSlctAWake.isLineArrow = true;
            g_cfgSlctAWake.isLabel = false;
            g_cfgSlctAWake.labelBackColor = System.Drawing.Color.Empty;
            g_cfgSlctAWake.labelForeColor = System.Drawing.Color.Empty;
        }

        //航跡レイヤ生成
        private void GenerateWakeLayer(
            ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> refDictWake,
            ref WakeCfg refWakeCongfig
            )
        {
            if (refDictWake == null) { return; }
            //レイヤ生成
            m_cUctrlMap.GenerateLayer(refWakeCongfig.sLyrName);
            //スタイル設定
            SetStyleToLayer(ref refWakeCongfig);
            //点オブジェクト追加
            if (refWakeCongfig.isPoint) { AddPointObject(ref refDictWake, ref refWakeCongfig); }
            //線オブジェクト追加
            if (refWakeCongfig.isLine) { AddLineObject(ref refDictWake, ref refWakeCongfig); }
            //ラベルレイヤ生成
            if (refWakeCongfig.isLabel) { GenerateLabelLayer(ref refDictWake, ref refWakeCongfig); }
        }

        //スタイル設定（レイヤの点の色など）
        private void SetStyleToLayer(ref WakeCfg refWakeCongfig)
        {
            //レイヤ取得(参照)
            VectorLayer layer = m_cUctrlMap.GetVectorLayerByName(refWakeCongfig.sLyrName);

            if (refWakeCongfig.isPoint)
            {
                //ポイントの色、サイズを設定
                layer.Style.PointColor = refWakeCongfig.pointColor;
                layer.Style.PointSize = refWakeCongfig.pointSize;
            }
            if (refWakeCongfig.isLine)
            {
                //ラインの色、太さを設定
                layer.Style.Line = new Pen(refWakeCongfig.lineColor, refWakeCongfig.lineWidth);

                if (refWakeCongfig.isLineDash)
                {
                    //破線にする { 破線の長さ, 間隔 }
                    layer.Style.Line.DashPattern = new float[] { 3.0F, 3.0F };
                }

                if (refWakeCongfig.isLineArrow)
                {
                    //矢印にする (width, height, isFilled)
                    layer.Style.Line.CustomEndCap = new System.Drawing.Drawing2D.AdjustableArrowCap(4f, 4f, true);
                }
            }
        }

        //点オブジェクト追加
        private void AddPointObject(
            ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> refDictWake,
            ref WakeCfg refWakeCongfig
            )
        {
            //wakeを取得
            foreach (var wake in refDictWake)
            {
                string row = null;
                string starttime = null;
                string endtime = null;
                int cnt = 0;
                List<Coordinate> listCoordinats = new List<Coordinate>();
                //座標を取得
                foreach (var pos in wake.Value)
                {
                    if (pos.Key.Contains("info")) { row = pos.Value["row"]; } //行を取得
                    if (pos.Key.Contains("pos")) //Keyが"pos"を含む
                    {
                        //取得した座標をリストに追加
                        Coordinate coordinate = new Coordinate(double.Parse(pos.Value["x"]), double.Parse(pos.Value["y"]));
                        listCoordinats.Add(coordinate);
                        //開始、終了時刻を取得
                        if (cnt == 1) { starttime = pos.Value["time"]; }
                        if (cnt == wake.Value.Count - 1) { endtime = pos.Value["time"]; }
                    }
                    cnt++;
                }
                //配列に変換
                Coordinate[] coordinates = listCoordinats.ToArray();
                //ユーザーデータ作成
                string userdata = "{row:" + row + ",starttime:" + starttime + ",endtime:" + endtime + "}";
                //レイヤーにオブジェクトを追加
                m_cUctrlMap.AddPointToLayer(refWakeCongfig.sLyrName, coordinates, userdata);
            }
        }

        //線オブジェクト追加
        private void AddLineObject(
            ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> refDictWake,
            ref WakeCfg refWakeCongfig
            )
        {
            //wakeを取得
            foreach (var wake in refDictWake)
            {
                string row = null;
                string starttime = null;
                string endtime = null;
                int cnt = 0;
                //座標リストを作成
                List<Coordinate> listCoordinate = new List<Coordinate>();
                foreach (var pos in wake.Value)
                {
                    if (pos.Key.Contains("info")) { row = pos.Value["row"]; } //行を取得
                    if (pos.Key.Contains("pos")) //Keyが"pos"を含む
                    {
                        //取得した座標をリストに追加
                        Coordinate coordinate = new Coordinate(double.Parse(pos.Value["x"]), double.Parse(pos.Value["y"]));
                        listCoordinate.Add(coordinate);
                        //開始、終了時刻を取得
                        if (cnt == 1) { starttime = pos.Value["time"]; }
                        if (cnt == wake.Value.Count - 1) { endtime = pos.Value["time"]; }
                    }
                    cnt++;
                }
                //配列に変換
                Coordinate[] coordinates = listCoordinate.ToArray();
                //ユーザーデータ作成
                string userdata = "{row:" + row + ",starttime:" + starttime + ",endtime:" + endtime + "}";
                //レイヤーにオブジェクトを追加
                m_cUctrlMap.AddLineToLayer(refWakeCongfig.sLyrName, coordinates, userdata);
            }
        }

        //ラベルレイヤ生成
        private void GenerateLabelLayer(
            ref Dictionary<string, Dictionary<string, Dictionary<string, string>>> refDictWake,
            ref WakeCfg refWakeCongfig
            )
        {
            VectorLayer lyr = null;
            LabelLayer llyr = null;
            {
                //カラム生成 {カラム1,カラム2,…}。 下記では0列目をラベル用とした
                System.Data.DataColumn[] columns = new[] { new System.Data.DataColumn("Label", typeof(string)) };

                //ラベルレイヤの生成
                {
                    //ラベル位置レイヤ生成し、カラムを設定
                    var fdt = new FeatureDataTable();
                    fdt.Columns.AddRange(columns);
                    lyr = new VectorLayer(refWakeCongfig.sLyrName + "LabelsPos", new GeometryFeatureProvider(fdt));

                    //ラベルレイヤ生成、ラベル位置レイヤのDataSourceとカラムを共有する
                    var lblLayer = new LabelLayer(refWakeCongfig.sLyrName + "Labels");
                    lblLayer.DataSource = lyr.DataSource; //ターゲットレイヤのDataSourceを共有
                    lblLayer.LabelColumn = columns[0].ColumnName; //0列目をラベル用として紐づけ
                    llyr = lblLayer;

                    //ラベルレイヤのスタイルを設定
                    llyr.Style.BackColor = new SolidBrush(refWakeCongfig.labelBackColor);//ラベル背景色
                    llyr.Style.ForeColor = Color.White;//ラベル文字色
                    llyr.Style.CollisionDetection = false; //true = ラベルが衝突するときは片方を非表示 , false = ラベルが衝突しても重ねて表示
                    llyr.Style.HorizontalAlignment = SharpMap.Styles.LabelStyle.HorizontalAlignmentEnum.Left; //水平位置合わせ(Left,Center,Right)
                    llyr.Style.VerticalAlignment = SharpMap.Styles.LabelStyle.VerticalAlignmentEnum.Middle; //垂直位置合わせ(Top,Middle,Bottom)

                    m_cUctrlMap.mapBox.Map.Layers.Add(llyr);
                }
            }
            //wakeを取得
            foreach (var wake in refDictWake)
            {
                foreach (var pos in wake.Value)
                {
                    if (pos.Key == "pos1")
                    {
                        Coordinate wpos = new Coordinate(double.Parse(pos.Value["x"]), double.Parse(pos.Value["y"]));
                        //ラベルに記載する文字列
                        string strLabelText = Regex.Replace(wake.Key, @"[^0-9]", "");
                        //オブジェクト生成
                        {
                            //ポイント(ジオメトリ)生成
                            var geom = new NetTopologySuite.Geometries.Point(wpos); //点
                                                                                    //データソースのFeaturesに新しい行を生成
                            var fp = (GeometryFeatureProvider)llyr.DataSource;
                            var fdr = fp.Features.NewRow();
                            fdr[0] = strLabelText; //★ 0列目にラベル(文字列)を代入
                            fdr.Geometry = geom; //ジオメトリを設定
                            fp.Features.AddRow(fdr);
                        }
                        break;
                    }
                }
            }
        }

    }
}
