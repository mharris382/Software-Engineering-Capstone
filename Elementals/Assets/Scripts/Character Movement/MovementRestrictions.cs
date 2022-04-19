namespace DefaultNamespace
{
    public enum MovementRestrictions
    {
        FullMovement = 0,
        NoMovementInput = 1 << 1,
        NoMovementPhysics = 1 << 2
    }
}