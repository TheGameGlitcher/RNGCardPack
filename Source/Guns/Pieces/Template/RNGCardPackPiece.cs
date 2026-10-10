using LBoL.ConfigData;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Resource;
using RNGCardPack.Source.Config;


namespace RNGCardPack.Source.Guns.Pieces.Template
{
    public class RNGCardPackPiece : PieceTemplate
    {
        public override PieceConfig MakeConfig()
        {
            return RNGCardPackDefaultConfig.DefaultPieceConfig();
        }

        public override IdContainer GetId()
        {
            return MakeConfig().Id;
        }

        public enum BulletColor
        {
            /// <summary>
            ///  No color modification
            /// </summary>
            Default = 0,
            /// <summary>
            /// // 0° hue
            /// </summary>
            Red = 1,
            /// <summary>
            /// 30° hue
            /// </summary>
            RedOrange = 2,
            /// <summary>
            /// 60° hue
            /// </summary>
            Orange = 3,
            /// <summary>
            /// 90° hue
            /// </summary>
            Yellow = 4,
            /// <summary>
            /// 120° hue
            /// </summary>
            YellowGreen = 5,
            /// <summary>
            /// 150° hue
            /// </summary>
            Lime = 6,
            /// <summary>
            /// 180° hue
            /// </summary>
            Cyan = 7,
            /// <summary>
            /// 210° hue
            /// </summary>
            SkyBlue = 8,
            /// <summary>
            /// 240° hue
            /// </summary>
            Blue = 9,
            /// <summary>
            /// 270° hue
            /// </summary>
            Purple = 10,
            /// <summary>
            /// 300° hue
            /// </summary>
            Magenta = 11,
            /// <summary>
            /// 330° hue
            /// </summary>
            Pink = 12,
            /// <summary>
            /// Desaturated/grayscale
            /// </summary>
            Grayscale = 13,
            /// <summary>
            /// Random hue (0-360°)
            /// </summary>
            RandomHue = 14,
            /// <summary>
            /// Random from 12 colors (0°, 30°, 60°, etc.)
            /// </summary>
            RandomPalette = 15
        }

        public enum BulletEventType
        {
            /// <summary>
            /// Modifies Bullet Speed
            /// </summary>
            Speed = 1,
            /// <summary>
            /// Modifies bullet angle/direction
            /// </summary>
            Angle = 2,
            /// <summary>
            /// Modifies acceleration
            /// </summary>
            Acceleration = 3,
            /// <summary>
            /// Modifies acceleration angle
            /// </summary>
            AccelerationAngle = 4,
            /// <summary>
            /// Aims toward target.
            /// If its event mode is 1 (transition), then it will instantly snap to the target.
            /// Use the special homing method for it.
            /// </summary>
            Homing = 5,
            /// <summary>
            /// Bounces off screen boundaries (mirror reflection). Does not bounce off the wall on the opposite side of the shooter.
            /// If its event mode is 1, it will aim towards the target at reflection instead.
            /// Use special bouncing method for it.
            /// </summary>
            BounceReflect = 9,
            /// <summary>
            /// Bounces to cardinal directions at boundaries (hitting the bottom changes its angle straight up). Does not bounce off the wall on the opposite side of the shooter.
            /// If its event mode is 1, it will aim towards the target at reflection.
            /// Use special bouncing method for it.
            /// </summary>
            BounceCardinal = 10,
            /// <summary>
            /// Directly changes X position
            /// </summary>
            PositionX = 11,
            /// <summary>
            /// Directly changes Y position
            /// </summary>
            PositionY = 12,
            /// <summary>
            /// Changes both X and Y scale
            /// </summary>
            ScaleUniform = 13,
            /// <summary>
            /// Changes Y scale only
            /// </summary>
            ScaleY = 14,
            /// <summary>
            /// Changes X scale only
            /// </summary>
            ScaleX = 15,
            /// <summary>
            /// Moves forward in bullet's direction
            /// </summary>
            MoveForward = 16,
            /// <summary>
            /// Moves perpendicular to bullet's direction
            /// </summary>
            MovePerpendicular = 17,
            /// <summary>
            /// Moves forward in AccAngle direction.
            /// Used for bullets that want to have a different direction to its sprite compared to its actual movement direction.
            /// For example, spinning objects.
            /// </summary>
            MoveAccAngleForward = 18,
            /// <summary>
            /// Moves perpendicular to AccAngle direction
            /// </summary>
            MoveAccAnglePerpendicular = 19,
            /// <summary>
            /// Custom sine-based movement. Ignores duration.
            /// </summary>
            Huali = 99
        }
    }
}
