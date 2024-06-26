language core;

// a constant function, specialized to Nat
fn Nat::applyToZero(f : fn(Nat) -> (fn(Nat) -> Nat)) -> (fn(Nat) -> Nat) {
    return f(0)
}

// addition of natural numbers
fn Nat::add(n : Nat) -> (fn(Nat) -> Nat) {
  return fn(m : Nat) {
    return Nat::rec(n, m,
    Nat::applyToZero(
        fn(i : Nat) {
              return fn(r : Nat) { return succ(r) } }
    ))
  }
}

fn main(n : Nat) -> Nat {
  return 0
}