# interaction data

class_name Interaction extends Resource


# Requirements:
# - define the changes in stats

@export var name: String
@export var icon: Texture2D
@export var description: String
@export var tab_category: Constants.Tab_Categories   # Constants.Tab_Category
@export var locked: bool
@export var stats_changes: Dictionary


# Make sure that every parameter has a default value.
# Otherwise, there will be problems with creating and editing
# your resource via the inspector.
func _init(_n = "", _i = null, _d = "", _t = Constants.Tab_Categories.FOOD,
		_l = true, _s = {"hunger": 100, "fun": 100, "health": 100}):
	name = _n
	icon = _i
	description = _d
	tab_category = _t
	locked = _l
	stats_changes = _s
