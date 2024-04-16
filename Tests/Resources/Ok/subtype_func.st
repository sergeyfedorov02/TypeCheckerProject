language core;

extend with
  #records,
  #structural-subtyping, #unit-type;

fn f(r : fn({x : Nat, y : Nat}) -> {x : Nat, y : Nat}) -> Unit {
  return unit
}

fn main(n : Nat) -> Unit {
  return f(fn(a : {x : Nat}) {
    return { x = 0, y = 0, z = 0 }
  });
}