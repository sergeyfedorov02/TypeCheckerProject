language core;

extend with  #let-patterns, #pattern-ascriptions, #let-bindings, #letrec-bindings, #lists;

fn foo(n : Nat) -> Bool {
  return true
}

fn main(n : Nat) -> Nat {
    return letrec x as Nat = foo(x)
    in x
}