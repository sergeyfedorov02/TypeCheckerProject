language core;

extend with #structural-subtyping, #multiparameter-functions;

fn foo(n : Nat) -> (fn(Nat) -> Nat) {
	return fn(x : Nat) { return x }
}

fn main(n : Nat) -> (fn(Nat, Nat) -> Nat) {
  return foo(n)
}
