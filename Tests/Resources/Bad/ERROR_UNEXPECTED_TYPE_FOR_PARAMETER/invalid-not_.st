language core;

fn main(x : Nat) -> Nat {
    return (Nat::rec(x, false, fn(i : Nat) { return fn(r : fn(Nat) -> Nat) { return fn(x : Nat) { return 0 } } }))(0)
}
