extends Node

var optionsScene = preload("res://scenes/Options.tscn")
var optionsMenu


# Called when the node enters the scene tree for the first time.
func _ready():
	optionsMenu = optionsScene.instantiate()
	add_child(optionsMenu)
	optionsMenu.visible = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
	
func _input(event):
	if event is InputEventKey and event.pressed:
		if event.keycode == KEY_ESCAPE:
			optionsMenu.visible = !optionsMenu.visible
	
