language core;
extend with
  #structural-subtyping,
  #references,
  #records;

fn main(x : &{ b : Bool }) -> &{ b : Bool, a : Nat } {
  return x
}

