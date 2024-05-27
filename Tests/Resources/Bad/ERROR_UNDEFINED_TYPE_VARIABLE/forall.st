language core;

extend with #universal-types;

generic fn const[X](x : X) -> forall Y. fn(C) -> X {
	return generic [Y] fn(y : Y) { return x }
}

fn main(x : Nat) -> Nat {
	return const[Nat](x)[Bool](false)
}