language core;
extend with #structural-patterns, #unit-type;

fn main(input : Unit) -> Bool {
  return
    match input {
      unit => true
      | 0 => false
   }
}