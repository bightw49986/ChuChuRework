public enum ERenderMode
{
    Opaque,
    Cutout,
    Fade,        // Old school alpha-blending mode, fresnel does not affect amount of transparency
    Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
}

namespace ChuChu
{
    public enum EObjectType
    {
        Other = 0,
        UI,
        DynamicSceneObj,
        FX,
        NPC
    }

    public enum EScene
    {
        None = 0,
        Title,
        Loading,
        Game
    }


}

