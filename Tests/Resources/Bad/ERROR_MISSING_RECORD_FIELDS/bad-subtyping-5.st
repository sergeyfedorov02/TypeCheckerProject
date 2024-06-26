language core;

extend with
  #records,
  #structural-subtyping,
  #top-type,
  #type-cast;

fn f(r : {x : Nat}) -> Nat {
  return r.x
}

fn g(k : fn({y : Nat, x : Nat}) -> Nat) -> Nat {
  return k({x = 0, y = 0})
}

fn h(k : fn(fn({z : Nat, y : Nat}) -> Nat) -> Nat) -> Nat {
  return k( fn(r : {z : Nat, y : Nat}) { return (r cast as {x : Nat}).x } )
}

fn main(n : Nat) -> Nat {
  return h(g)
}
