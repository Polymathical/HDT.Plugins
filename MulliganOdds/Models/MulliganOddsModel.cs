
namespace DeckTrackerCustom
{

    public class MulliganOddsModel
    {
        public double LowerOdds { get; set; }
        public double EqualOdds { get; set; }
        public double HigherOdds { get; set; }

        public MulliganOddsModel(double lowerOdds, double equalOdds, double higherOdds)
        {
            lowerOdds = LowerOdds;
            EqualOdds = equalOdds;
            higherOdds = HigherOdds;
        }
    }
}