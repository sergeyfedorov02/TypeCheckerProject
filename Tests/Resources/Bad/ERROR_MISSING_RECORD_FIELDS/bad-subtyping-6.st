language core;
extend with #structural-subtyping, #records;

fn main(n : Nat) -> fn({ x : Nat }) -> Nat {
  return fn(a : { x : Nat, y : Nat }) {
    return 0
  }
}