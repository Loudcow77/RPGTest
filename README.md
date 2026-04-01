# RPGTest
<a name="japanese"></a>
# 概要
本プロジェクトは、UnityとC#を用いてRPGのコアゲームプレイシステムをゼロから実装することを目的とした、自主制作の技術デモです。アートやコンテンツの作り込みではなく、ほぼすべてのRPGに存在するシステム — インベントリ・装備・ステータス管理 — を、保守性・拡張性の高いコードで設計・実装することに重点を置きました。

![alt text](image.jpg)

## 実装システム
**インベントリシステム**

- スロットベースのアイテムコンテナ（容量は設定可能）
- アイテムを ScriptableObject のデータアセットとして定義し、データとロジックを分離
- スタック可能／不可能なアイテムタイプに両対応
- 追加・削除・スワップ・ドロップの全操作を実装
- シーン遷移をまたいだインベントリ状態の永続化

**装備システム**

- スロット種別による装備バリデーション（武器・防具・アクセサリ）
- 装備・解除時にキャラクターステータスを自動更新
- 不正な装備を防止（例：武器を2本同時装備する操作のブロック）
- 装備マネージャーとステータスシステムの疎結合設計 — 変更はイベントコールバック経由で伝播し、直接参照には依存しない

**キャラクターステータスシステム**

- ステータス（HP・ATK・DEF・SPD）を基本値 ＋ モディファイアスタックで管理
- 装備による加算・乗算モディファイアを独立して適用
- 解除時はモディファイアをクリーンに削除し、ステータスのドリフト（ズレ）が発生しない設計
- 拡張性を考慮した設計：既存ロジックを変更せずに新しいステータス種別を追加可能

**UI層**

- アイテムデータからインベントリグリッドを動的レンダリング
- ツールチップにアイテムステータスと現在装備品との比較差分を表示
- 装備パネルは装備・解除に応じてステータス変化をリアルタイム反映

## アーキテクチャ・設計方針
| パターン | 使用箇所 |
| ----------- | ----------- |
| ScriptableObject（データコンテナ） | アイテム定義 — MonoBehaviourロジックからデータを分離 |
| Observer / イベントシステム | 装備変更によるステータス更新を疎結合で通知 |
| Singleton | GameManager によるグローバル状態管理 |
| 単一責任の原則 | インベントリ・装備・ステータスを独立したマネージャーとして分割し、相互依存を排除 |

設計上最も工夫が必要だったのはステータスモディファイアスタックの実装です。モディファイアをどの順序で適用・削除しても残留値が生じないようにするため、毎回再計算するのではなく「モディファイアを発生源ごとに追跡して管理する」方式を採用しました。

| 技術スタック | |
| ----------- | ----------- |
| エンジン | Unity (LTS) |
| 言語 | C# |
| アーキテクチャ | コンポーネントベース ＋ ScriptableObjectデータ層 |
| バージョン管理 | Git |

## 学んだこと
- 既存コードを変更せずに拡張できるデータドリブンなアイテムシステムの設計方法
- ステータス更新におけるイベント駆動とポーリングのトレードオフの実践的な理解
- UIをデータへの純粋なリアクションとして設計し、ゲーム状態を持たせない構造の重要性
- 所有権の境界を明確に定義したシステム設計 — チーム開発において特に重要な考え方

プレイ (itch.io): loudcow67.itch.io/rpg-test
<a name="english"></a>
# Overview
This project is a self-directed technical demo designed to practice and demonstrate core RPG gameplay systems from scratch in Unity using C#. The focus was not on art or content, but on writing clean, extensible code for systems that appear in almost every RPG — inventory, equipment, and stat management.

## Systems Implemented
** Inventory System**

- Slot-based item container with configurable capacity
- Items are represented as ScriptableObject data assets, separating data from logic
- Supports stackable and non-stackable item types
- Full add / remove / swap / drop operations
- Inventory state persists across scene transitions

**Equipment System**

- Slot-typed equipping (Weapon, Armour, Accessory) with validation logic
- Equipping/unequipping an item automatically updates character stats
- Prevents invalid equips (e.g. equipping two weapons simultaneously)
- Clean separation between the equipment manager and the stat system — changes propagate via event callbacks, not direct coupling

**Character Stat System**

- Stats (HP, ATK, DEF, SPD) stored as a base value + modifier stack
- Equipment applies additive and multiplicative modifiers independently
- Modifiers are removed cleanly on unequip with no stat drift
- Extensible design: new stat types can be added without modifying existing logic

**UI Layer**

- Inventory grid rendered dynamically from item data
- Tooltips display item stats and comparison delta vs. currently equipped item
- Equipment panel reflects live stat changes on equip/unequip

## Architecture & Design Decisions
| Pattern | Where Used |
| ----------- | ----------- |
| ScriptableObject (Data Container) | Item definitions — decouples data from MonoBehaviour logic |
| Observer /  Event System | Stat updates triggered by equipment changes without tight coupling |
| Singleton | GameManager for global state access |
| Single Responsibility | Inventory, Equipment, and Stats are separate managers with no cross-dependency |

The stat modifier stack was the most interesting design challenge — ensuring modifiers could be applied and removed in any order without leaving residual values required tracking modifiers by source rather than recalculating from scratch each time.

| Technical Stack| |
| ----------- | ----------- |
| Engine | Unity (LTS) |
| Language | C# |
| Architecture | Component-based with ScriptableObject data layer |
| Version Control | Git |

## What I Learned
- How to design a data-driven item system that is easy to extend without touching existing code
- The practical tradeoffs between event-driven vs. polling approaches for stat updates
- How to structure UI so it is purely reactive to underlying data rather than owning any game state
- The importance of writing systems with clear ownership boundaries — a lesson directly applicable to team-based game development

プレイ (itch.io): loudcow67.itch.io/rpg-test
