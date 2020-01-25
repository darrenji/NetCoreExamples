为了flexible和maintain,组件loose coupling, 依赖倒置可做到。

- 高层模块不能依赖低层模块，而应该依赖抽象或接口。就像DDD中的领域层属于高层模块，不依赖于任何具体类。
- 抽象不能依赖具体，具体依赖抽象。

来看一个高层模块。

```
public class AuthenticationManager
{
    //依赖太具体
    private DbContext dbContext;

    public AuthenticationManager(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}
```

应该依赖接口。
```
public interface IDbContext
{
    int SaveChanges();
    void Dispose();
}

public class DbContext : IDbContext
{
    public int SaveChanges()
    {

    }

    public void Dispose()
    {

    }
}

public class AuthenticationManager
{
    private IDbContext dbContext;

    public AudhtnenticationManager(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}
```

