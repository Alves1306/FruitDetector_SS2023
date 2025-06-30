using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FruitDLL
{


    public enum FruitEnum
    {
        unknown,
        banana,
        maca,
        pera,
        maca_red,
        limao,
        figo,
        morango
    };
    public enum FeaturesEnum
    {
        Factor_forma,
        Circularidade,
        Quociente_aspecto,
        Convexidade,
        Solidez,
        Compactacao,
        Racio_modificacao,
        Extensao
    }
    
    public enum MetricFeaturesEnum
    {
        area,
        diametro_maximo,
        diametro_minimo
    }


    public class Fruit
    {
        string delimeterA = "$";
        string delimeterB = "&";
        string delimeterC = "=";

        //nivel 1
        public FruitEnum fruitEnum = FruitEnum.unknown; // identificação da fruta
        public Dictionary<FeaturesEnum, float> features = new Dictionary<FeaturesEnum, float>();  //lista de 3 caracteristicas usadas para classificação no nivel 1, 2 e 3
        public Rectangle fruitRect; //Rectangulo que envolve o fruto
        public Point fruitCentroid; //centroide do objecto
                                    //nivel 3
        public Dictionary<MetricFeaturesEnum, float> metricFeatures = new Dictionary<MetricFeaturesEnum, float>();  // lista de caracteristicas calculadas em unidades metricas 

        // nivel 4 
        public Dictionary<string, float> aditionalFeatures = new Dictionary<string, float>(); //lista de caracteristicas usadas para classificação no nivel 4

        /// <summary>
        /// simple constructor
        /// </summary>
        public Fruit() { }

        /// <summary>
        /// load constructor
        /// </summary>
        /// <param name="info"></param>
        public Fruit(string info)
        {
            string[] infos = info.Split(delimeterA.ToCharArray());

            fruitEnum = (FruitEnum)Enum.Parse(typeof(FruitEnum), infos[0].Remove(0,1));

            //rectangle 
            char[] delimitersRect = { ',', '=' };
            string[] rectStr = infos[1].Substring(1, infos[1].Length-2).Split(delimitersRect);
            fruitRect = new Rectangle(int.Parse(rectStr[1]), int.Parse(rectStr[3]), int.Parse(rectStr[5]), int.Parse(rectStr[7]));

            char[] delimitersInfo = { '(', '{', delimeterB.ToCharArray()[0], '=',')','}' };
            string[] infosMetric = infos[2].Substring(1,infos[2].Length-4).Split(delimeterB.ToCharArray());
            foreach (string infoMetric in infosMetric)
            {
                string[] featureStr = infoMetric.Substring(1,infoMetric.Length-2).Split(delimeterC.ToCharArray());
                metricFeatures.Add(
                    (MetricFeaturesEnum)Enum.Parse(typeof(MetricFeaturesEnum), featureStr[0]),
                    float.Parse(featureStr[1]));

            }


        }

        public override string  ToString()
        {
            StringBuilder sbFeatures = new StringBuilder();
            foreach (FeaturesEnum feature in features.Keys)
            {
                sbFeatures.Append("(" + feature.ToString() + "=" + features[feature].ToString() + ")" + delimeterB);
            }

            StringBuilder sbMetric = new StringBuilder();
            foreach (MetricFeaturesEnum metricFeature in metricFeatures.Keys)
            {
                sbMetric.Append("(" + metricFeature.ToString() + "=" + metricFeatures[metricFeature].ToString() + ")" + delimeterB);
            }
            StringBuilder sbAditional = new StringBuilder();
            foreach (string aditionalFeature in aditionalFeatures.Keys)
            {
                sbAditional.Append("(" + aditionalFeature + "=" + aditionalFeatures[aditionalFeature].ToString() + ")" + delimeterB);
            }
            return "(" + fruitEnum.ToString() + delimeterA + fruitRect.ToString() + delimeterA +
                "{" + sbFeatures.ToString() + "}" + delimeterA +
                "{" + sbMetric.ToString() + "}" + delimeterA +
                "{" + sbAditional.ToString() + "})";
        }


        public string ToString_SS_Files()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MetricFeaturesEnum metricFeature in metricFeatures.Keys)
            {
                sb.Append("(" + metricFeature.ToString() + "=" + metricFeatures[metricFeature].ToString() + ")" + delimeterB);
            }

            return "(" + fruitEnum.ToString() + delimeterA + fruitRect.ToString() + delimeterA + "{" + sb.ToString() + "})";
        }
    }



    public class FruitResults
    {
        string delimeter = "|";
        public List<Fruit> results = new List<Fruit>();
        public int referenceWidth = -1; // largura em pixeis do objeto de referência (losango)

        public override string ToString()
        {
            string result = "";
            foreach (Fruit fruit in results)
            {
                result += fruit.ToString() + delimeter;
            }
            return result + referenceWidth;
        }

        public string ToString_SS_Eval()
        {
            string result = "";
            foreach (Fruit fruit in results)
            {
                result += fruit.ToString_SS_Files() + delimeter;
            }
            return result + referenceWidth;
        }

        public FruitResults() { }

        public FruitResults(string info)
        {
            string[] fruits = info.Split(delimeter.ToCharArray());

            referenceWidth = int.Parse(fruits.Last<string>());


            for (int i = 0; i < fruits.Length - 1; i++)
            {
                results.Add(new Fruit(fruits[i]));

            }
        }
    }


}