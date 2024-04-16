language core;

extend with
  #variants,
  #structural-subtyping, #unit-type;

fn f(r : <| a : Bool, b : Nat |>) -> Unit {
  return unit
}

fn main(n : <|a : Bool|>) -> Unit {
  return f(n);
}