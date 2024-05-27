language core;

extend with #universal-types, #type-cast;

generic fn identity[A, B](x : A) -> B {
	return x cast as B
}

fn main(x : Nat) -> Bool {
	return identity[Nat, Bool](x)
}