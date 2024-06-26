language core;

// addition of natural numbers
fn Nat::add(n : Nat) -> (fn(Nat) -> Nat) {
  return fn(m : Nat) {
    return Nat::rec(n, m, fn(r : Nat) { return succ(r) })
  }
}

fn main(n : Nat) -> Nat {
  return 0
}