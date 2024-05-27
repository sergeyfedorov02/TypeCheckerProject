language core;

extend with #universal-types;

generic fn const[X](x : X) -> forall X. fn(X) -> X {
	return generic [X] fn(y : X) { return x }
}

fn main(x : Nat) -> Bool {
	return const[Nat](x)[Bool](false)
}
