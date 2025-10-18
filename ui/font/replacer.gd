@tool
extends RichTextEffect
class_name Replacer

var bbcode = "replacer"

func _process_custom_fx(char_fx: CharFXTransform) -> bool:
  var simple = char_fx.env.get("simple", false)
  var replacement = char_fx.env.get("prim_repl", ["t","e","s","t", " "])
  var sec_repl = char_fx.env.get("sec_repl", ["æ", "e", "c"])
  var speed = char_fx.env.get("speed", 1.0)
  var text_server = TextServerManager.get_primary_interface()
  
  var wave = tan(sin(char_fx.elapsed_time * speed)*1.5) *0.8 +0.8
  
  if(wave <= 12 and wave >= 6 || wave <= -4 and wave >= -10):
    if(randi() % 2 == 0):
      char_fx.glyph_index = text_server.font_get_glyph_index(char_fx.font, 16, replacement[char_fx.relative_index % len(replacement)].unicode_at(0), 0)
    else: 
      char_fx.glyph_index = text_server.font_get_glyph_index(char_fx.font, 16, sec_repl[char_fx.relative_index % len(sec_repl)].unicode_at(0), 0)
      

  return true
