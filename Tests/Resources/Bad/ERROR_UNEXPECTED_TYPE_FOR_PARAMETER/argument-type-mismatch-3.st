language core;

fn iszero(n : Nat) -> Bool {
    return Nat::rec(n, true, fn(i : Nat) {
        return fn(r : Bool) {
            return true
        }
    })
}

fn k(g : fn(Nat) -> (fn(fn(Nat) -> Bool) -> Nat)) -> Nat {
    return g(succ(succ(0)))(fn(k : Nat) {
        return if iszero(k) then false else true
    })
}

fn main(n : Nat) -> Nat {
  return k( fn(n : Nat) {
      return fn(h : fn(Nat) -> Nat) {
        return h(succ(n))
      }
  })
}
