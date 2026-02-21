extends Control

var parent : Node
var optionsScene = preload("res://scenes/Options.tscn")
var optionsMenu

# Called when the node enters the scene tree for the first time.
func _enter_tree():
	parent = $VBoxContainer
	$VBoxContainer/Play.grab_focus()
	optionsMenu = optionsScene.instantiate()
	parent.add_child(optionsMenu)
	optionsMenu.visible = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_play_pressed():
	get_tree().change_scene_to_file("res://scenes/Main.tscn")


func _on_options_pressed():
	optionsMenu.visible = !optionsMenu.visible


func _on_quit_pressed():
	get_tree().quit()
