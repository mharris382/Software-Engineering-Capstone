namespace Projectiles
{
    public interface IProjectileDecorator
    {
        void OnProjectileFired(RigidbodyProjectile projectile);
    }
}