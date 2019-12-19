namespace TianYu.Core.Common.UnitOfWork
{
    /// <summary>
    /// 聚合根接口，用作泛型约束，约束领域实体为聚合根，表示实现了该接口的为聚合根实例，由于聚合根也是领域实体的一种，所以也要实现IEntity接口
    /// </summary>
    public interface IBaseAggregateRoot : IEntity
    {
    }
}
