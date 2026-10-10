using LBoL.ConfigData;
using RNGCardPack.Source.Config;
using RNGCardPack.Source.Guns.Pieces.Template;

namespace RNGCardPack.Source.Guns.Pieces.SemiAutomatedDollP
{
    public sealed class SemiAutomatedDollPPiece0Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777010);

            config.Projectile = "DanmaBigMagic";

            config.ShootType = 1;

            config.LastWave = false;

            config.Scale = new float[][] { new float[] { 0.8f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.Grayscale } };

            config.RootType = 0;

            config.Aim = 1;

            config.StartTime = 0;

            config.GInterval = 6;

            config.Group = 1;

            config.Way = new int[][] { new int[] { 2 } };

            config.Life = new int[][] { new int[] { 35 } };

            config.Range = new float[][] { new float[] { 90f } };

            config.StartSpeed = new float[][] { new float[] { 15f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 10 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Speed, 1 } };

            return config;
        }
    }

    public sealed class SemiAutomatedDollPPiece2Def : RNGCardPackPiece
    {
        public override PieceConfig MakeConfig()
        {
            PieceConfig config = RNGCardPackDefaultConfig.DefaultPieceConfig();

            config.Id = ConvertGunId(777010, 1);

            config.Projectile = "DanmaFish";

            config.ShootType = 2;

            config.ParentPiece = 0;

            config.AddParentAngle = true;

            config.LastWave = true;

            config.Scale = new float[][] { new float[] { 1f } };

            config.Color = new int[][] { new int[] { (int)BulletColor.RandomHue } };

            config.Aim = 1;

            config.StartTime = 11;

            config.GInterval = 1;

            config.Group = 8;

            config.Way = new int[][] { new int[] { 1 } };

            config.Life = new int[][] { new int[] { 180 } };

            config.GAngle = new float[][] { new float[] { 0f, 180f } };

            config.StartSpeed = new float[][] { new float[] { 12f }, new float[] { 0f, 0.2f } };

            config.EvStart = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvDuration = new int[][][] { new int[][] { new int[] { 0 } } };

            config.EvNumber = new float[][][] { new float[][] { new float[] { 0f } } };

            config.EvType = new int[][] { new int[] { (int)BulletEventType.Homing } };

            return config;
        }
    }
}
