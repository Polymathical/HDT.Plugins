
namespace HDT.Plugins.Custom.Models
{
    public class CardInfoModel 
    {
        public int CardCost { get; set; }
        public double CardDrawPercent { get; set; }
        public double CardDrawRunningTotal { get; set; }

        public CardInfoModel() { }

        public CardInfoModel(int cost, double drawPercent, double runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }

       
    }
   
}