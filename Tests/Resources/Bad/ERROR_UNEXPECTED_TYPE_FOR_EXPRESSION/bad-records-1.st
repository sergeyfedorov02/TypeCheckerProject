language core;
extend with #records;

fn rotate3(p : {x : Nat, y : Bool, z : Nat}) -> {a : Nat, b : Nat, c : Nat} {
  return {a = p.z, b = p.y, c = p.x}
}

fn main(x : Nat) -> {a : Nat, b : Nat, c : Nat} {
  return rotate3({x = x, y = succ(x), z = succ(succ(x))})
}