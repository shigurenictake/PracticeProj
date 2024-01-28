using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace PracticeProj
{
    public partial class UctrlMap : UserControl
    {
        //コンストラクタ
        public UctrlMap()
        {
            //デザイン初期化
            InitializeComponent();

            //SharpMap初期化
            this.InitializeMap();
        }

        /// <summary>
        /// このユーザーコントロールがロードされたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UctrlMap_Load(object sender, EventArgs e)
        {
            // デザイン中は呼び出さない
            if (this.DesignMode) return;
        }

        /// <summary>
        /// MapBox初期化
        /// </summary>
        public void ClearMapBox()
        {
            //T.B.D.
        }

        //マップ初期化
        private void InitializeMap()
        {
            //baseLayerレイヤ初期化
            this.InitializeBaseLayer();

            //Zoom制限
            mapBox.Map.MinimumZoom = 0.1;
            mapBox.Map.MaximumZoom = 360.0;

            //レイヤ全体を表示する(全レイヤの範囲にズームする)
            mapBox.Map.ZoomToExtents();

            //mapBoxを再描画
            mapBox.Refresh();
        }

        //基底レイヤ初期化
        private void InitializeBaseLayer()
        {
            //Map生成
            mapBox.Map = new Map(new Size(mapBox.Width, mapBox.Height));
            mapBox.Map.BackColor = System.Drawing.Color.LightBlue;

            //レイヤーの作成
            VectorLayer baseLayer = new VectorLayer("baseLayer");

            try
            {
                baseLayer.DataSource = new ShapeFile(@"..\..\ShapeFiles\polbnda_jpn\polbnda_jpn.shp");
                //baseLayer.DataSource = new ShapeFile(@"..\..\ShapeFiles\ne_10m_coastline\ne_10m_coastline.shp");
            }
            catch//(Exception ex)
            {
                //開発中はこっち（カレントディレクトリがWakeMap.slnの階層になる）
                baseLayer.DataSource = new ShapeFile(@".\WakeMap\ShapeFiles\polbnda_jpn\polbnda_jpn.shp");
                //baseLayer.DataSource = new ShapeFile(@".\WakeMap\ShapeFiles\ne_10m_coastline\ne_10m_coastline.shp");
            }

            baseLayer.Style.Fill = Brushes.LimeGreen;
            baseLayer.Style.Outline = Pens.Black;
            baseLayer.Style.EnableOutline = true;

            //マップにレイヤーを追加
            mapBox.Map.Layers.Add(baseLayer);
        }

        //再描画
        public void MapBoxRefresh()
        {
            //mapBoxを再描画
            mapBox.Refresh();
        }

        /// <summary>
        /// ベース以外の全レイヤ削除
        /// </summary>
        public void InitLayerOtherThanBase()
        {
            //ベース(0番目)以外のレイヤ削除
            while (mapBox.Map.Layers.Count > 1)
            {
                mapBox.Map.Layers.RemoveAt((mapBox.Map.Layers.Count - 1));
            }
            //mapBoxを再描画
            mapBox.Refresh();
        }

        //レイヤ生成
        public void GenerateLayer(string layername)
        {
            //レイヤ生成
            VectorLayer layer = new VectorLayer(layername);
            //ジオメトリ生成
            List<IGeometry> igeoms = new List<IGeometry>();
            //ジオメトリをレイヤに反映
            GeometryProvider gpro = new GeometryProvider(igeoms);
            layer.DataSource = gpro;
            //layer.Style.PointColor = Brushes.Red;
            //layer.Style.Line = new Pen(Color.DarkRed, 1.0f);
            //レイヤをmapBoxに追加
            mapBox.Map.Layers.Add(layer);
        }

        /// <summary>
        /// VectorLayer型でレイヤ取得
        /// メリット：DataSourceを参照できる
        /// </summary>
        /// <param name="layername"></param>
        /// <returns></returns>
        public VectorLayer GetVectorLayerByName(string layername)
        {
            VectorLayer retlayer = null;
            LayerCollection layers = mapBox.Map.Layers;
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].GetType().ToString() == "SharpMap.Layers.VectorLayer")
                {
                    VectorLayer layer = (VectorLayer)layers[i];
                    if (layer.LayerName == layername)
                    {
                        retlayer = layer;
                        break;
                    }
                }

            }
            return retlayer;

            //使用例
            //{
            //    //指定した領域()の特徴を返す Envelope( x1 , x2 , y1, y2)
            //    Collection<IGeometry> geoms =
            //        rlayer.DataSource.GetGeometriesInView(
            //            new GeoAPI.Geometries.Envelope(130, 140, 30, 40) //経度130～140, 緯度30～40で囲まれる四角形
            //        );
            //    foreach (IGeometry geom in geoms) { Console.WriteLine(geom); }
            //}

        }

        /// <summary>
        /// レイヤ内の全ジオメトリ（地図上に配置した LineString や Point など）を取得
        /// 範囲:地図全体(経度-180～180, 緯度-90～90で囲まれる四角形)
        /// </summary>
        /// <param name="layer"></param>
        public Collection<IGeometry> GetIGeometriesAllByVectorLayer(VectorLayer layer)
        {
            if (layer == null) { return null; }

            //指定した領域の特徴を返す Envelope( x1 , x2 , y1, y2)
            //地図全体(経度-180～180, 緯度-90～90で囲まれる四角形)
            Collection<IGeometry> igeoms =
                layer.DataSource.GetGeometriesInView(
                    new GeoAPI.Geometries.Envelope(-180, 180, -90, 90)
                );
            return igeoms;

            //使用例
            //{
            //    //レイヤ内の全ジオメトリを取得
            //    VectorLayer layer = sharpMapHelper.GetVectorLayerByName(mapBox, "pointLayer");
            //    Collection<IGeometry> igeoms = sharpMapHelper.GetIGeometriesAllByVectorLayer( layer );
            //    //ジオメトリ一覧を表示
            //    string text = string.Empty;
            //    for (int i = 0; i < igeoms.Count; i++) { text = text + $"[ {i} ] : {igeoms[i]}" + "\n"; }
            //    Console.WriteLine(text);
            //}

        }

        //指定レイヤにPoint追加
        public void AddPointToLayer(string layername, Coordinate[] worldPos, string userdata)
        {
            //レイヤ取得
            VectorLayer layer = GetVectorLayerByName(layername);
            //ジオメトリ取得
            Collection<IGeometry> igeoms = GetIGeometriesAllByVectorLayer(layer);
            //点をジオメトリに追加
            GeometryFactory gf = new GeometryFactory();
            IMultiPoint ipoint = gf.CreateMultiPointFromCoords(worldPos);
            ipoint.UserData = userdata;
            //ジオメトリのコレクションに追加
            igeoms.Add(ipoint);
            //ジオメトリをレイヤに反映
            GeometryProvider gpro = new GeometryProvider(igeoms);
            layer.DataSource = gpro;
        }

        //ラインを追加
        public void AddLineToLayer(string layername, Coordinate[] coordinates, string userdata)
        {
            //レイヤ取得
            VectorLayer layer = GetVectorLayerByName(layername);
            //ジオメトリ取得
            Collection<IGeometry> igeoms = GetIGeometriesAllByVectorLayer(layer);
            //図形生成クラス
            GeometryFactory gf = new GeometryFactory();
            //座標リストの線を生成し、ジオメトリのコレクションに追加
            ILineString ilinestring = gf.CreateLineString(coordinates);
            ilinestring.UserData = userdata;
            //ジオメトリのコレクションに追加
            igeoms.Add(ilinestring);
            //ジオメトリをレイヤに反映
            GeometryProvider gpro = new GeometryProvider(igeoms);
            layer.DataSource = gpro;
        }

    }
}
