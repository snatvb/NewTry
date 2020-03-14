using Systems;
using Animation;
using Camera;
using EcsCollisionHandler;
using Enemy;
using Enemy.Systems;
using Guns.Components;
using Guns.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using Player.Systems;
using UnityEngine;

[DisallowMultipleComponent]
public class Setup : MonoBehaviour {
  private EcsWorld world;
  private EcsSystems systems;
  private EcsSystems fixedSystems;

  // Field that should be initialized by instance of `EcsUiEmitter` assigned to Ui root GameObject.
  [SerializeField] private EcsUiEmitter uiEmitter;
  [SerializeField] private LayerMask groundLayer = 8;
  [SerializeField] private PlayerInitData playerInitData;
  [SerializeField] private EnemyInitData enemyInitData;

  private void Initialize() {
    world = new EcsWorld();
    systems = new EcsSystems(world);
    fixedSystems = new EcsSystems(world);
    
    #if DEBUG
    new Leopotam.Ecs.RemoteDebug.RemoteDebugClient(world);
    #endif
    #if UNITY_EDITOR
    Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
    Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
    #endif

    RegisterAutoReset();


    fixedSystems
     .RegisterCollisionEmitter()
     .Add(new PlayerRunSystem())
     .Add(new FixRampPositionSystem())
     .Add(new GroundCheckSystem(groundLayer))
     .Add(new EnemyShootSystem())
     .Init();

    systems
     .Add(new CameraMoveSystem())
     .Add(new CameraFollowSystem())
     .Add(new PlayerSpawnSystem(playerInitData))
     .Add(new EnemySpawnSystem(enemyInitData))
     .Add(new PlayerInputSystem())
     .Add(new PlayerJumpSystem())
     .Add(new AnimationMoveSystem())
     .Add(new PlasmaShotSystem())
     .OneFrame<PlasmaShotComponent>()
      #if UNITY_EDITOR
     .Add(new LogCollisionsSystems())
      #endif
     .Init();
  }

  private void RegisterAutoReset() {
    world
     .GetPool<CameraTargetFollowComponent>()
     .SetAutoReset(CameraTargetFollowComponent.CustomReset);
  }

  private void Awake() {
    DontDestroyOnLoad(this);
    Initialize();
  }

  private void Update() {
    systems.Run();
  }

  private void FixedUpdate() {
    fixedSystems.Run();
  }

  void OnDestroy() {
    systems.Destroy();
    systems = null;
    fixedSystems.Destroy();
    fixedSystems = null;
    world.Destroy();
    world = null;
  }
}