
namespace HDT.Plugins.MassiveDynamic
{
    public class CardModel  
    {
        public CardModel(int cost, double drawPercent, double runningTotal)
        {
            CardCost = cost;
            CardDrawPercent = drawPercent;
            CardDrawRunningTotal = runningTotal;
        }

        public int CardCost { get; set; }
        public double CardDrawPercent { get; set; }
        public double CardDrawRunningTotal { get; set; }
       
    }
   
}