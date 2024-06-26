language core;
extend with #type-cast-patterns, #structural-subtyping, #records;

fn main(x : { a : Nat, b : Nat }) -> Bool {
  return match x {
      y cast as { a : Nat } => true
    | _ => false
  }
}