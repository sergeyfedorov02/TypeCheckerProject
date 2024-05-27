language core;

extend with #universal-types, #type-cast;

generic fn identity[A, B](x : C) -> B {
	return x cast as B
}

fn main(x : Nat) -> Bool {
	return identity[Nat, Bool](x)
}