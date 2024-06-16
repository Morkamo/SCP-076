# SCP-076
Плагин добавляет на ваш сервер новую игровую роль - SCP-076
SCP-076 прдеставляет собой "Custom Role" и новой оружие "Custom Item" типа "Jailbird"

**Характеристики "SCP-076 (Custom Role)"**
```
- Является союзником с командой SCP
  - Не может получать или наносить урон или воздействовать на SCP
  - SCP не могут применять на нём свои способности
  - Способен затягивать раунд если остались враждебные таргеты
  - Является дополнительным таргетом для людей и пополняет список SCP на восстановление условий содержания
  - Автоматически заменяет одного игрока из команды SCP при появлении

- Автоматическое появление в начала каждого раунда определяют 3 показателя:
  - SpawnChance - (Шанс появления)
  - MinPlayersForSpawn - (Минимальное количество игроков в раунде)
  - MinScpUnits-  (Минимальное количество SCP в раунде)

- Имеет независимые параметры скорости передвижения - MovementBoost (0 - 255%)
- Имеет настраиваемый список разрешенных на подбор предметов
- Имеет настраиваемую причину смерти для таргетов
```

**Характеристики "SCP-076 (Custom Item)"**
```
- Оружие типа "Jailbird", функционал реализуется только в руках SCP-076
- Его нельзя выбросить из инвентаря
- Его нельзя переработать в SCP-914
- Доступна настройка урона на обычную и заряженную атаку
- Оружие не выпадает после смерти владельца
```

**Config :**
```
SCP-076:
  is_enabled: true
  debug: false
  # Увеличивает скорость передвижения игрока ( 0 - 255% )
  movement_boost: 75
  # Разрешает SCP-914 перерабатывать Item076 в другой
  can_upgrade_in914: false
  # Разрешает игроку выбрасывать Item076
  can_drop: false
  # Урон наносимый обычным ударом Item076
  melee_damage: 100
  # Урон наносимый заряженным ударом Item076
  charge_damage: 500
  # Указывает причину смерти от SCP-076
  death_reason: 'На теле обнаружены огромные резаные раны!'
  # Указывает шанс спавна SCP-076 в начале раунда
  spawn_chance: 100
  # Указывает требуемое минимальное количество игроков на сервере для автоспавна
  min_player_for_spawn: 10
  # Минимальное количество живых SCP в раунде
  min_scp_units: 5
  # Список разрешенных на подбор предметов
  allowed_items_list:
  - Medkit
  - Painkillers
  - Adrenaline
  - SCP500
  - ArmorLight
  - ArmorCombat
  - ArmorHeavy
  - KeycardJanitor
  - KeycardScientist
  - KeycardResearchCoordinator
  - KeycardZoneManager
  - KeycardGuard
  - KeycardMTFPrivate
  - KeycardMTFOperative
  - KeycardMTFCaptain
  - KeycardChaosInsurgency
  - KeycardContainmentEngineer
  - KeycardFacilityManager
  - KeycardO5
  role076:
    id: 1
    role: Tutorial
    max_health: 700
    name: 'SCP-076'
    description: 'Its SCP-076'
    custom_info: 'SCP-076'
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points: []
    scale:
      x: 1
      y: 1
      z: 1
    inventory:
    - 'Item076'
    custom_abilities: []
    ammo: {}
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
  item076:
    id: 1
    weight: 1
    name: 'Item076'
    description: 'Item for SCP-076'
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points: []
    type: Jailbird
    scale:
      x: 1
      y: 1
      z: 1
```
