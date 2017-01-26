
namespace HDT.Plugins.Custom.Models
{
    public class CardModel 
    {
        public int CardCost { get; set; }
        public double CardDrawPercent { get; set; }
        public double CardDrawRunningTotal { get; set; }

        public CardModel() { }

        public CardModel(int cost, double drawPercent, double runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }
       
    }
   
}