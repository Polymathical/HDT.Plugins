
namespace DeckTrackerCustom
{

    public class MulliganOddsModel
    {
        public double LowerOdds { get; set; }
        public double EqualOdds { get; set; }
        public double HigherOdds { get; set; }

        public MulliganOddsModel(double lowerOdds, double equalOdds, double higherOdds)
        {
            LowerOdds = lowerOdds;
            EqualOdds = equalOdds;
            HigherOdds = higherOdds;
        }
    }
}