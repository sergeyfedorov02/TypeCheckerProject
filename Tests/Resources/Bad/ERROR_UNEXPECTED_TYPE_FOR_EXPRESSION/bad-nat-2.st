// ERROR_UNEXPECTED_TYPE_FOR_PARAMETER
language core;

fn main(n : Nat) -> Nat {
  return Nat::rec(false, 0, fn(i : Nat) {
      return fn(r : Nat) {
        return succ(r)
      }
  })
}