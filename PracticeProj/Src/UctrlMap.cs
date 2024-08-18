using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using SharpMap;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PracticeProj
{
    public partial class UctrlMap : UserControl
    {
        ICoordinateTransformation transformation;
        ICoordinateTransformation reverseTransformation;

        //コンストラクタ
        public UctrlMap()
        {
            //デザイン初期化
            InitializeComponent();

            //SharpMap初期化
            this.InitializeMap();

            //描画
            Draw();

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
            mapBox.Map.MinimumZoom = 0.1 * 1500000;
            mapBox.Map.MaximumZoom = 360.0 * 1500000;

            //レイヤ全体を表示する(全レイヤの範囲にズームする)
            mapBox.Map.ZoomToExtents();

            //mapBoxを再描画
            mapBox.Refresh();
        }

        //基底レイヤ初期化
        private void InitializeBaseLayer()
        {
            // メルカトル図法の座標系を定義
            var geographicCS = GeographicCoordinateSystem.WGS84;
            var mercatorCS = ProjectedCoordinateSystem.WebMercator;

            // 座標変換ファクトリを作成
            var transformFactory = new CoordinateTransformationFactory();
            transformation = transformFactory.CreateFromCoordinateSystems(geographicCS, mercatorCS);
            reverseTransformation = transformFactory.CreateFromCoordinateSystems(mercatorCS, geographicCS);

            // SharpMapのマップオブジェクトを作成
            var map = new Map(mapBox.Size)
            {
                BackColor = System.Drawing.Color.LightBlue,
                SRID = 3857 // EPSG:3857はWebメルカトルのSRID
            };

            mapBox.Map = map;

            //=================================================================

            //レイヤーの作成
            VectorLayer baseLayer = new VectorLayer("baseLayer");
            try
            {
                baseLayer.DataSource = new ShapeFile(@"..\..\ShapeFiles\ne_10m_land\ne_10m_land.shp", true);
            }
            catch//(Exception ex)
            {
                //デザインタブで見るときはこっち（カレントディレクトリが***.slnの階層になる）
                baseLayer.DataSource = new ShapeFile(@".\PracticeProj\ShapeFiles\ne_10m_land\ne_10m_land.shp");//, true);
            }
            baseLayer.SRID = 4326; // データソースのSRIDを設定

            baseLayer.Style.Fill = Brushes.LimeGreen;
            baseLayer.Style.Outline = Pens.Black;
            baseLayer.Style.EnableOutline = true;

            //=================================================================

            // 座標変換を設定
            baseLayer.CoordinateTransformation = transformation;
            baseLayer.ReverseCoordinateTransformation = reverseTransformation;

            // マップに追加
            mapBox.Map.Layers.Add(baseLayer);

            //=================================================================
        }

        private void Draw()
        {
            //Coordinate[] coords = new Coordinate[] {
            //    new Coordinate(-170, 85),
            //    new Coordinate(-175, 80),
            //    new Coordinate(-165, 75),
            //    new Coordinate( 179, 70),
            //    new Coordinate(-179, 65),
            //    new Coordinate( 180, 60),
            //    new Coordinate(-180, 55),
            //};

            Coordinate[] coords = new Coordinate[]
            {
                new Coordinate(-175, 85),
                new Coordinate(-175, 85),
                new Coordinate(-175, 80),
                new Coordinate(179, 75),
                new Coordinate(-179, 70),
                new Coordinate(180, 65),
                new Coordinate(-180, 60)
            };

            List<List<Coordinate>> LineList = new List<List<Coordinate>>();

            GenerateLayer("LayerA");
            AddLineToLayer("LayerA", coords, "テスト");
            mapBox.Refresh();
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
            //ベース(0,1,2番目)以外のレイヤ削除
            while (mapBox.Map.Layers.Count > 3)
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
            
            layer.Style.PointColor = Brushes.Red;
            layer.Style.Line = new Pen(Color.DarkRed, 1.0f);

            // 座標変換を設定
            layer.CoordinateTransformation = transformation;
            layer.ReverseCoordinateTransformation = reverseTransformation;

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
                    new GeoAPI.Geometries.Envelope(-540, 540, -90, 90)
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
            ProcessCoordinates(coordinates);

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


        //▼=================================================================================
        // 方位角を計算するヘルパーメソッド
        private static double CalculateBearing(Coordinate start, Coordinate end)
        {
            double lat1 = DegToRad(start.Y);
            double lat2 = DegToRad(end.Y);
            double dLon = DegToRad(end.X - start.X);

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            double bearing = RadToDeg(Math.Atan2(y, x));
            return (bearing + 360) % 360; // 0 <= bearing < 360
        }

        // 度からラジアンへの変換
        private static double DegToRad(double degrees) => degrees * Math.PI / 180;

        // ラジアンから度への変換
        private static double RadToDeg(double radians) => radians * 180 / Math.PI;

        // 方位角の差を計算するヘルパーメソッド
        private static double BearingDifference(double bearing1, double bearing2)
        {
            double diff = Math.Abs(bearing1 - bearing2) % 360;
            return diff > 180 ? 360 - diff : diff; // 0 <= difference <= 180
        }

        public static void ProcessCoordinates(Coordinate[] coords)
        {
            List<List<Coordinate>> LineList = new List<List<Coordinate>>();
            int workLine = 0;
            int nextLine = 1;

            // 初期化: 最初のラインを作成
            LineList.Add(new List<Coordinate>());

            for (int i = 0; i < coords.Length; i++)
            {
                if (i == 0)
                {
                    // 初回はそのまま追加
                    LineList[workLine].Add(coords[i]);

                    // シフトラインの作成
                    if (coords[i].X < 0)
                    {
                        LineList.Add(new List<Coordinate> { new Coordinate(coords[i].X + 360, coords[i].Y) });
                    }
                    else
                    {
                        LineList.Add(new List<Coordinate> { new Coordinate(coords[i].X - 360, coords[i].Y) });
                    }
                }
                else if (i == 1)
                {
                    // 経度の差を計算
                    double diff = coords[i].X - coords[i - 1].X;

                    if (Math.Abs(diff) > 180)
                    {
                        if (diff > 0)
                        {
                            coords[i].X += 360;
                        }
                        else
                        {
                            coords[i].X -= 360;
                        }
                    }

                    LineList[workLine].Add(coords[i]);

                    // 跨ぎを処理
                    if ((LineList[workLine][i - 1].X < 0 && coords[i].X >= 0) ||
                        (LineList[workLine][i - 1].X >= 0 && coords[i].X < 0))
                    {
                        if (LineList[workLine][i - 1].X < 0 && coords[i].X >= 0)
                        {
                            LineList.Add(new List<Coordinate>
                        {
                            new Coordinate(LineList[workLine][i - 1].X + 360, LineList[workLine][i - 1].Y),
                            new Coordinate(coords[i].X - 360, coords[i].Y)
                        });
                        }
                        else
                        {
                            LineList.Add(new List<Coordinate>
                        {
                            new Coordinate(LineList[workLine][i - 1].X - 360, LineList[workLine][i - 1].Y),
                            new Coordinate(coords[i].X + 360, coords[i].Y)
                        });
                        }
                        nextLine++;
                    }
                }
                else
                {
                    // 方位角を計算
                    double currentBearing = CalculateBearing(LineList[workLine][i - 2], LineList[workLine][i - 1]);
                    double futureBearing = CalculateBearing(LineList[workLine][i - 1], coords[i]);

                    // 経度の差を計算
                    double diff = coords[i].X - LineList[workLine][i - 1].X;

                    // 経度が±180度を跨ぐ場合
                    if (Math.Abs(diff) > 180)
                    {
                        double originalChange = BearingDifference(currentBearing, futureBearing);
                        double shiftedChange;

                        if (diff > 0)
                        {
                            // +360度シフト
                            var temp = coords[i];
                            temp.X += 360;
                            shiftedChange = BearingDifference(currentBearing, CalculateBearing(LineList[workLine][i - 1], temp));
                            if (shiftedChange < originalChange)
                            {
                                coords[i].X += 360;
                            }
                        }
                        else
                        {
                            // -360度シフト
                            var temp = coords[i];
                            temp.X -= 360;
                            shiftedChange = BearingDifference(currentBearing, CalculateBearing(LineList[workLine][i - 1], temp));
                            if (shiftedChange < originalChange)
                            {
                                coords[i].X -= 360;
                            }
                        }
                    }

                    LineList[workLine].Add(coords[i]);

                    // 跨ぎを処理
                    if ((LineList[workLine][i - 1].X < 0 && coords[i].X >= 0) ||
                        (LineList[workLine][i - 1].X >= 0 && coords[i].X < 0))
                    {
                        if (LineList[workLine][i - 1].X < 0 && coords[i].X >= 0)
                        {
                            LineList.Add(new List<Coordinate>
                        {
                            new Coordinate(LineList[workLine][i - 1].X + 360, LineList[workLine][i - 1].Y),
                            new Coordinate(coords[i].X - 360, coords[i].Y)
                        });
                        }
                        else
                        {
                            LineList.Add(new List<Coordinate>
                        {
                            new Coordinate(LineList[workLine][i - 1].X - 360, LineList[workLine][i - 1].Y),
                            new Coordinate(coords[i].X + 360, coords[i].Y)
                        });
                        }
                        workLine = nextLine;
                        nextLine++;
                    }
                }
            }

            // 結果の表示
            for (int j = 0; j < LineList.Count; j++)
            {
                Console.WriteLine($"Line {j + 1}:");
                foreach (var coord in LineList[j])
                {
                    Console.WriteLine($"  {coord}");
                }
            }
        }

        //▲=================================================================================

    }
}
