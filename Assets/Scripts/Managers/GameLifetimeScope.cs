using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        //例
        // // MonoBehaviourを登録
        // MonoBehaviour someComponent;
        // builder.RegisterComponent(someComponent);
        // SerializeFiledで登録することも可能です
        // // シーン上のコンポーネントを登録
        // builder.RegisterComponentInHierarchy<FooBehaviour>();
        // // シーン上のコンポーネントのインターフェースを登録
        // builder.RegisterComponentInHierarchy<FooBehaviour>().AsImplementedInterfaces();
        // // Prefabをインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped);
        // // Prefabを親を指定してインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped).UnderTransform(parentTransform);
        // builder.RegisterComponentInNewPrefab(somePrefab, Lifetime.Scoped).UnderTransform(() => parentTransform);
        // // GameObjectをインスタンス化してそのコンポーネントを登録
        // builder.RegisterComponentOnNewGameObject<FooBehaviour>(Lifetime.Scoped, "ObjectName");
    }
}
