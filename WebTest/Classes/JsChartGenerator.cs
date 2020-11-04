using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebTest.Classes
{
    public class JsChartGenerator : Chart<string, string>
    {
        ////////////////////////////////////////////////
        /// TODO: JsChartGenerator:internal
        //////////////////////////////////////////////// 

        class HTMLFrame
        {
            public string Width { get; set; }
            public string Height { get; set; }
            public string ID { get; set; }
            public string BorderWidth { get; set; }
            public string BorderLine { get; set; }
            public string BorderColor { get; set; }

            public string CanvasForm =>
            $@"
                <canvas
                id     = ""{ID}""
                style  = ""
                    border: {BorderWidth}px {BorderLine} {BorderColor};
                    width:  {Width};    
                    height: {Height};
                         ""
               ></canvas>
            ";
        }
        class JSFrame
        {
            ////////////////////////////////////////////////
            /// TODO: JSFrame:const
            ////////////////////////////////////////////////  

            public const string PREFIX = "<script>";
            public const string POSTFIX = "</script>";
            public const string SCRIPT = "function draw() {";
            public const string _SCRIPT = " }";

            public string init;
            public List<string> Script { get; set; }
            public List<string> Buffer { get; set; }
            string draw;

            public void Draw()
            {
                draw = string.Concat(Buffer);
                Script.AddRange(new string[] { PREFIX, SCRIPT, init, draw, _SCRIPT, POSTFIX });
            }
        }
        public class DatasetParameters
        {
            string Label { get; set; } = default;
            string LineTension { get; set; } = default;
            string Fill { get; set; } = default;
            string BorderColor { get; set; } = default;
            string BackgroundColor { get; set; } = default;
            IEnumerable<string> BorderDash { get; set; } = default;
            string BointBorderColor { get; set; } = default;
            string PointBackgroundColor { get; set; } = default;
            string PointRadius { get; set; } = default;
            string PointHoverRadius { get; set; } = default;
            string PointHitRadius { get; set; } = default;
            string PointBorderWidth { get; set; } = default;
            string PointStyle { get; set; } = default;

            public override string ToString()
            {
                return
                    $@"
                         label: ""{Label}"",                                //name
                         lineTension: {LineTension},                        //int
                         fill: {Fill},                                      //bool
                         borderColor: '{BorderColor}',                      //color name or rgba(0 - 255,0,0,0)
                         backgroundColor: '{BackgroundColor}',              //color name or rgba(0 - 255,0,0,0)
                         borderDash: [{string.Concat(BorderDash.Select(l => l + ","))}],
                         pointBorderColor: '{BointBorderColor}',            //color name or rgba(0 - 255,0,0,0)
                         pointBackgroundColor: '{PointBackgroundColor}',    //color name or rgba(0 - 255,0,0,0)
                         pointRadius: {PointRadius},                        //int
                         pointHoverRadius: {PointHoverRadius},              //int
                         pointHitRadius: {PointHitRadius},                  //int
                         pointBorderWidth: {PointBorderWidth},              //int
                         pointStyle: '{PointStyle}'                         //style name
                    ";
            }
        }

        ////////////////////////////////////////////////
        /// TODO: JsChartGenerator:fields
        ////////////////////////////////////////////////  

        HTMLFrame html_frame;
        JSFrame js_frame;

        ////////////////////////////////////////////////
        /// TODO: JsChartGenerator:constructors
        ////////////////////////////////////////////////
        ///
        public JsChartGenerator(string height = "500",
                                string width = "500",
                                string id = "canvas",
                                string borderWidth = "1",
                                string borderline = "solid",
                                string borderColor = "black")
        {
            string GetPrefix(string s) => string.Concat(s.Where(c => char.IsDigit(c)));
            string GetPostfix(string s) => string.Concat(s.Where(c => char.IsLetter(c)));
            string GetRGBAColor(Color color) => $"rgba({color.R}, {color.G}, {color.B}, {color.A})";
            string GetCanvasColor(Color color) => $"ctx.fillStyle = \"{GetRGBAColor(color)}\";";

            html_frame = new HTMLFrame()
            {
                BorderColor = GetRGBAColor(Color.FromName(borderColor)),
                ID = id,
                BorderWidth = borderWidth + "",
                BorderLine = borderline,
                Width = int.TryParse(width, out int wres) ? wres + "px" : width,
                Height = int.TryParse(height, out int hres) ? hres + "px" : height,
            };
            js_frame = new JSFrame()
            {
                Script = new List<string>(),
                Buffer = new List<string>(),
                init = $"var canvas = document.getElementById('{id}');" +
                    $"var ctx = canvas.getContext('2d');" +
                    $"canvas.width = {GetPrefix(html_frame.Width)} * window.devicePixelRatio;" +
                    $"canvas.height = {GetPrefix(html_frame.Height)} * window.devicePixelRatio;" +
                    $"canvas.style.width = {GetPrefix(html_frame.Width)} + '{GetPostfix(html_frame.Width)}';" +
                    $"canvas.style.height = {GetPrefix(html_frame.Height)} + '{GetPostfix(html_frame.Height)}';"
            };

            js_frame.Buffer.Add(GetCanvasColor(Color.Black));

            SetChart(new List<(string, string)>() { ("1", "1"), ("2", "2"), ("3", "3") }); //test

            js_frame.Draw();
        }

        ////////////////////////////////////////////////
        /// TODO: JsChartGenerator:props
        //////////////////////////////////////////////// 

        public string GetCanvasScript => string.Concat(js_frame.Script);
        public string GetCanvasForm => $"{html_frame.CanvasForm}";
        public string GetCanvas => $"{GetCanvasForm}{GetCanvasScript}";

        ////////////////////////////////////////////////
        /// TODO: JsChartGenerator:methods
        //////////////////////////////////////////////// 
        string GetDataSet(IEnumerable<string> data, DatasetParameters parameters)
        {
            return @$" var dataFirst = {{
                            data: [{string.Concat(data.Select(l => l + ","))}],
                            {parameters.ToString()}
                        }};";
        }

        string AddDataLayer(IEnumerable<(string, string)> points, int setIndex, string label)
        {
            var values = "";

            foreach (var point in points)
                values += $"'{point.Item1}',";

            return $@"  var data{setIndex} = 
                        {{
                            label: ""{label}"",
                            data:  {values}
                        }}
            ";
        }
        public void SetChart(IEnumerable<(string, string)> points)
        {
            var labels = "";
            var data = "";

            foreach (var point in points)
                labels += $"'{point.Item2}',";
            foreach (var point in points)
                data += $"{point.Item1},";

            string field =
                 @$"
                    new Chart(ctx, {{
                        type: 'line',
                        data: {{
                            // Точки графиков
                            labels: [{labels}],
                            // График
                            datasets: [{{
                                label: 'Мой первый график на Chart.js', 
                                backgroundColor: 'rgb(255, 99, 132)', 
                                borderColor: 'rgb(255, 99, 132)', 
                                data: [{data}]  
                            }}]
                        }},
                        options: {{}}
                        }});
                   
                ";
            js_frame.Buffer.Add(field);
        }
    }
}
