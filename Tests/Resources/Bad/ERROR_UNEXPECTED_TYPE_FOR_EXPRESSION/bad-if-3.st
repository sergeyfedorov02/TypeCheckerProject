language core;

fn main(x : Nat) -> Nat {
    return (if false then fn(x : Nat) { return x } else fn(x : Nat) { return x })(false)
}