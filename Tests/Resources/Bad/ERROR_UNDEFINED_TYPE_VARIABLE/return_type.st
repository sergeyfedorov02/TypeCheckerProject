language core;

extend with #universal-types, #type-cast;

generic fn identity[A, B](x : A) -> C {
	return x cast as C
}

fn main(x : Nat) -> Bool {
	return identity[Nat, Bool](x)
}