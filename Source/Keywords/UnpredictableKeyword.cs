using LBoLEntitySideloader.CustomKeywords;
using RNGCardPack.Source.StatusEffects.Dummies;

namespace RNGCardPack.Source.Keywords
{
    public static class UnpredictableKeyword
    {
        public static CardKeyword Unpredictable = new CardKeyword(nameof(DummyUnpredictableSe)) { descPos = KwDescPos.First };
    }
}
