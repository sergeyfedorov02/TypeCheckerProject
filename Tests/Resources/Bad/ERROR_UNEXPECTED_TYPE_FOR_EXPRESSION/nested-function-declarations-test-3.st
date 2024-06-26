language core;

extend with #nested-function-declarations;

// factorial via primitive recursion
fn factorial(n : Nat) -> Nat {
  // a constant function, specialized to Nat
  fn Nat2Nat::const(f : fn(Nat) -> Nat) -> (fn(Nat) -> Nat) {
    return f
  }

  // multiplication of natural numbers
  fn Nat::mul(n : Nat) -> (fn(Nat) -> Nat) {
    // addition of natural numbers
    fn Nat::add(n : Nat) -> (fn(Nat) -> Nat) {
      // a constant function, specialized to Nat
      fn Nat2Nat::const(f : fn(Nat) -> Nat) -> (fn(Nat) -> (fn(Nat) -> Nat)) {
        return fn(x : Nat) { return f }
      }

      return fn(m : Nat) {
        return Nat::rec(n, m, fn(i : Nat) {
          return fn(r : Nat) { return succ(r) } })
      }
    }

    return fn(m : Nat) {
      return Nat::rec(n, 0, Nat2Nat::const(Nat::add(m)))
    }
  }

  return Nat::rec(n, succ(0), fn(i : Nat) {
    return fn(r : Nat) {
    return Nat::mul(r)(succ(i))  // r := r * (i + 1)
  } })
}

fn main(n : Nat) -> Nat {
  return factorial(n)
}