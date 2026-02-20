extends Node

@export var food_items: Array[Interaction]
@export var activity_items: Array[Interaction]
var tab_container: TabContainer
var select_sprite_offset: Vector2
var selection_active: bool
var left_mouse_clicked: bool
var selected_interaction: Interaction
var pet
const INTERACT_RANGE = 120


# Called when the node enters the scene tree for the first time.
func _ready():
	tab_container = get_node("BottomBar/TabContainer")
	pet = get_node("../Pet")
	_build_ui()


func _input(event):
	if event is InputEventMouseButton and event.pressed:
		match event.button_index:
			MOUSE_BUTTON_LEFT:
				left_mouse_clicked = true
	else:
		left_mouse_clicked = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Make selection sprite follow
	var mouse_pos = get_viewport().get_mouse_position()
	var dist_mouse_to_pet = mouse_pos.distance_to(pet.position)
	$SelectionSprite.position = mouse_pos + select_sprite_offset
	
	# Handle mouse clicks
	if left_mouse_clicked && selection_active:
		if dist_mouse_to_pet < INTERACT_RANGE:
			_on_drop_interact(selected_interaction)
			selection_active = false
		else:
			print("Deselect interaction.")
			selection_active = false
	
	if !selection_active:
		$SelectionSprite.texture = null
		selected_interaction = null


func _build_ui():
	# build food tab
	var food_tab = HBoxContainer.new()
	tab_container.add_child(food_tab)
	tab_container.set_tab_title(0, "Food")
	tab_container.set_tab_icon(0, load("res://art/items/food/apple.png"))
	tab_container.set_tab_icon_max_width(0, 50)
	
	# add buttons to food tab
	for i in food_items:
		var button = Button.new()
		button.icon = i.icon
		button.text = i.name
		# add button functionality
		button.pressed.connect(_on_interact_pressed.bind(i))
		
		food_tab.add_child(button)
	
	# build activity tab
	var activities_tab = HBoxContainer.new()
	# add button for each relevant piece
	tab_container.add_child(activities_tab)
	tab_container.set_tab_title(1, "Activities")
	tab_container.set_tab_icon(1,
		load("res://art/items/toys_activities/crayon box.png"))
	tab_container.set_tab_icon_max_width(1, 50)
	
	for i in activity_items:
		var button = Button.new()
		button.icon = i.icon
		button.text = i.name
		# add button functionality
		button.pressed.connect(_on_interact_pressed.bind(i))
		
		# add button for each relevant piece
		activities_tab.add_child(button)

# Handle interaction button press
func _on_interact_pressed(interaction:Interaction):
	print("Interact was pressed on interactable " + interaction.name + ".")
	selection_active = true
	$SelectionSprite.texture = interaction.icon
	selected_interaction = interaction

# Handle dropping interaction on Little Thing
func _on_drop_interact(interaction:Interaction):
	print("Dropped interactable " + interaction.name + " on your pet.")
	
	if interaction.tab_category == Constants.Tab_Categories.FOOD:
		pet.eat(interaction)
