public enum InteractionPhysicsMode
{
    FullRootMotion = 0,  //default, root motion controls character's movement
    Mixed = 1,      //root motion is added with gravity
    FullPhysics = 2 //root motion is applied as a force
}